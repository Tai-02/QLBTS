using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBTS_DTO;
using QLBTS_DAL;

namespace QLBTS_BLL
{
    public class CartBLL
    {
        private CartDAL cartDAL;
        private ProductDAL productDAL;

        public CartBLL()
        {
            cartDAL = new CartDAL();
            productDAL = new ProductDAL();
        }

        /// <summary>
        /// Lấy giỏ hàng theo MaTK (gộp chung Customer + Employee)
        /// </summary>
        public List<CartItemViewModel> GetCart(int maTK)
        {
            if (maTK <= 0)
                throw new ArgumentException("Mã tài khoản không hợp lệ");

            try
            {
                return cartDAL.GetCart(maTK);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetCart: {ex.Message}", ex);
            }
        }

        public int GetTotalItems(List<CartItemViewModel> items)
        {
            return items?.Sum(x => x.SoLuong) ?? 0;
        }

        public decimal GetTotalAmount(List<CartItemViewModel> items)
        {
            return items?.Sum(x => x.ThanhTien) ?? 0;
        }

        public decimal GetTotalDiscount(List<CartItemViewModel> items)
        {
            return items?.Sum(x => x.TienGiam) ?? 0;
        }

        public bool UpdateQuantity(int maCTGH, int soLuong)
        {
            if (maCTGH <= 0)
            {
                throw new ArgumentException("Mã chi tiết giỏ hàng không hợp lệ");
            }

            if (soLuong < 1)
            {
                throw new ArgumentException("Số lượng phải >= 1");
            }

            try
            {
                return cartDAL.UpdateQuantity(maCTGH, soLuong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - UpdateQuantity: {ex.Message}", ex);
            }
        }

        public bool RemoveItem(int maCTGH)
        {
            if (maCTGH <= 0)
            {
                throw new ArgumentException("Mã chi tiết giỏ hàng không hợp lệ");
            }

            try
            {
                return cartDAL.DeleteCartItem(maCTGH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - RemoveItem: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ (gộp chung)
        /// </summary>
        public bool AddToCart(int maTK, int maSP, int soLuong)
        {
            if (maTK <= 0 || maSP <= 0)
            {
                throw new ArgumentException("Mã tài khoản hoặc mã sản phẩm không hợp lệ");
            }

            if (soLuong < 1)
            {
                throw new ArgumentException("Số lượng phải >= 1");
            }

            try
            {
                return cartDAL.AddToCart(maTK, maSP, soLuong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - AddToCart: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        public bool ClearCart(int maTK)
        {
            if (maTK <= 0)
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ");
            }

            try
            {
                return cartDAL.ClearCart(maTK);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - ClearCart: {ex.Message}", ex);
            }
        }



        /// <summary>
        /// Kiểm tra tồn kho trước khi checkout
        /// </summary>
        public string ValidateStockBeforeCheckout(List<CartItemViewModel> items)
        {
            if (items == null || !items.Any())
                return "Giỏ hàng trống!";


            foreach (var item in items)
            {
                try
                {
                    int tonKho = productDAL.GetCurrentStock(item.MaSP);

                    if (tonKho < item.SoLuong)
                    {
                        return $"Sản phẩm '{item.TenSP}' chỉ còn {tonKho} sản phẩm trong kho!";
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi BLL - ValidateStockBeforeCheckout: {ex.Message}", ex);
                }
            }

            return null; // OK
        }

        /// <summary>
        /// Checkout: Tạo đơn hàng từ giỏ
        /// </summary>
        public int Checkout(int maTK, out int tongTien)
        {
            if (maTK <= 0)
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ");
            }

            try
            {
                // Kiểm tra giỏ hàng
                var cartItems = GetCart(maTK);

                if (cartItems == null || !cartItems.Any())
                {
                    throw new Exception("Giỏ hàng trống!");
                }

                // Kiểm tra tồn kho
                string stockError = ValidateStockBeforeCheckout(cartItems);
                if (stockError != null)
                {
                    throw new Exception(stockError);
                }

                // Tạo đơn hàng
                int maDH = cartDAL.CreateOrderFromCart(maTK, out tongTien);

                return maDH;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - Checkout: {ex.Message}", ex);
            }
        }
    }
}
