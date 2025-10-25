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

        public CartBLL()
        {
            cartDAL = new CartDAL();
        }

        public List<CartItemViewModel> GetCartByCustomer(int maKH)
        {
            if (maKH <= 0)
                throw new ArgumentException("Mã khách hàng không hợp lệ");

            try
            {
                return cartDAL.GetCartByCustomer(maKH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetCartByCustomer: {ex.Message}", ex);
            }
        }

        public List<CartItemViewModel> GetCartByEmployee(int maNV)
        {
            if (maNV <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ");

            try
            {
                return cartDAL.GetCartByEmployee(maNV);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetCartByEmployee: {ex.Message}", ex);
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


        /// <summary>
        /// Cập nhật số lượng
        /// </summary>
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

        /// <summary>
        /// Xóa sản phẩm khỏi giỏ
        /// </summary>
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
        /// Thêm sản phẩm vào giỏ (Khách hàng)
        /// </summary>
        public bool AddToCartCustomer(int maKH, int maSP, int soLuong)
        {
            if (maKH <= 0 || maSP <= 0)
            {
                throw new ArgumentException("Mã khách hàng hoặc mã sản phẩm không hợp lệ");
            }

            if (soLuong < 1)
            {
                throw new ArgumentException("Số lượng phải >= 1");
            }

            try
            {
                return cartDAL.AddToCart(maKH, null, maSP, soLuong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - AddToCartCustomer: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ (Nhân viên)
        /// </summary>
        public bool AddToCartEmployee(int maNV, int maSP, int soLuong)
        {
            if (maNV <= 0 || maSP <= 0)
            {
                throw new ArgumentException("Mã nhân viên hoặc mã sản phẩm không hợp lệ");
            }

            if (soLuong < 1)
            {
                throw new ArgumentException("Số lượng phải >= 1");
            }

            try
            {
                return cartDAL.AddToCart(null, maNV, maSP, soLuong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - AddToCartEmployee: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng (Khách hàng)
        /// </summary>
        public bool ClearCartCustomer(int maKH)
        {
            if (maKH <= 0)
            {
                throw new ArgumentException("Mã khách hàng không hợp lệ");
            }

            try
            {
                return cartDAL.ClearCart(maKH, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - ClearCartCustomer: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng (Nhân viên)
        /// </summary>
        public bool ClearCartEmployee(int maNV)
        {
            if (maNV <= 0)
            {
                throw new ArgumentException("Mã nhân viên không hợp lệ");
            }

            try
            {
                return cartDAL.ClearCart(null, maNV);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - ClearCartEmployee: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kiểm tra giỏ hàng có trống không
        /// </summary>
        public bool IsCartEmpty(List<CartItemViewModel> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Kiểm tra tất cả sản phẩm còn hàng không
        /// </summary>
        public bool CheckAllItemsAvailable(List<CartItemViewModel> items)
        {
            if (items == null || !items.Any())
                return false;

            return items.All(x => x.ConHang);
        }

        /// <summary>
        /// Lấy danh sách sản phẩm hết hàng
        /// </summary>
        public List<CartItemViewModel> GetOutOfStockItems(List<CartItemViewModel> items)
        {
            if (items == null || !items.Any())
                return new List<CartItemViewModel>();

            return items.Where(x => !x.ConHang).ToList();
        }
    }
}
