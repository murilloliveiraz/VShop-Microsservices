using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Contexts;
using ShoppingCart.API.DTOs;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;
        private IMapper _mapper;

        public CartRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CleanCartAsync(string userId)
        {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            if(cartHeader is not null)
            {
                _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));
                _context.CartHeaders.Remove(cartHeader);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteItemCartAsync(int cartItemId)
        {
            try
            {
                CartItem cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id  == cartItemId);
                int total = _context.CartItems.Where(c => c.CartHeaderId == cartItem.CartHeaderId).Count();

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                if (total == 1)
                {
                    var cartHeaderRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderRemove);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CartDTO> GetCartByUserIdAsync(string userId)
        {
            Cart cart = new Cart
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
            };

            cart.CartItems = _context.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);

            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<CartDTO> UpdateCartAsync(CartDTO cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);

            await SaveProductInDataBase(cartDto, cart);

            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                                   c => c.UserId == cart.CartHeader.UserId);
            if (cartHeader is null)
            {
                await CreateCartHeaderAndItems(cart);
            }
            else
            {
                await UpdateQuantityAndItems(cartDto, cart, cartHeader);
            }
            return _mapper.Map<CartDTO>(cart);
        }

        private async Task UpdateQuantityAndItems(CartDTO cartDto, Cart cart,
    CartHeader? cartHeader)
        {
            var cartDetail = await _context.CartItems.AsNoTracking().FirstOrDefaultAsync(
                                   p => p.ProductId == cartDto.CartItems.FirstOrDefault()
                                   .ProductId && p.CartHeaderId == cartHeader.Id);
            if (cartDetail is null)
            {
                cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
                cart.CartItems.FirstOrDefault().Product = null;
                _context.CartItems.Add(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                cart.CartItems.FirstOrDefault().Product = null;
                cart.CartItems.FirstOrDefault().Quantity += cartDetail.Quantity;
                cart.CartItems.FirstOrDefault().Id = cartDetail.Id;
                cart.CartItems.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                _context.CartItems.Update(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }
        private async Task CreateCartHeaderAndItems(Cart cart)
        {
            _context.CartHeaders.Add(cart.CartHeader);
            await _context.SaveChangesAsync();
            cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartItems.FirstOrDefault().Product = null;
            _context.CartItems.Add(cart.CartItems.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
        private async Task SaveProductInDataBase(CartDTO cartDto, Cart cart)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id ==
                                cartDto.CartItems.FirstOrDefault().ProductId);
            if (product is null)
            {
                _context.Products.Add(cart.CartItems.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> ApplyCouponAsync(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCouponAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
