using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBTS_DTO;
using QLBTS_DAL;

namespace QLBTS_BLL
{
    public class OrderBLL
    {
        private OrderDAL orderDAL;

        public OrderBLL()
        {
            orderDAL = new OrderDAL();
        }

        /// <summary>
        /// Lấy chi tiết đơn hàng
        /// </summary>
        public List<OrderDetailViewModel> GetOrderDetails(int maDH)
        {
            if (maDH <= 0)
            {
                throw new ArgumentException("Mã đơn hàng không hợp lệ");
            }

            try
            {
                return orderDAL.GetOrderDetails(maDH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetOrderDetails: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tính tổng tạm tính (chưa giảm giá)
        /// </summary>
        public decimal GetSubTotal(List<OrderDetailViewModel> items)
        {
            return items?.Sum(x => x.ThanhTien) ?? 0;
        }

        /// <summary>
        /// Tính tổng giảm giá
        /// </summary>
        public decimal GetTotalDiscount(List<OrderDetailViewModel> items)
        {
            // Nếu có logic giảm giá phức tạp, thêm vào đây
            return 0; // Tạm thời return 0
        }

        /// <summary>
        /// Tính tổng cộng cuối cùng
        /// </summary>
        public decimal GetGrandTotal(List<OrderDetailViewModel> items)
        {
            decimal subTotal = GetSubTotal(items);
            decimal discount = GetTotalDiscount(items);
            return subTotal - discount;
        }

        /// <summary>
        /// Đếm tổng số sản phẩm
        /// </summary>
        public int GetTotalItems(List<OrderDetailViewModel> items)
        {
            return items?.Sum(x => x.SoLuong) ?? 0;
        }
        /// <summary>
        /// Lấy tất cả đơn hàng
        /// </summary>
        public List<OrderListViewModel> GetAllOrders()
        {
            try
            {
                return orderDAL.GetAllOrders();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetAllOrders: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy đơn hàng theo trạng thái
        /// </summary>
        public List<OrderListViewModel> GetOrdersByStatus(string trangThai)
        {
            if (string.IsNullOrWhiteSpace(trangThai))
            {
                throw new ArgumentException("Trạng thái không hợp lệ");
            }

            try
            {
                return orderDAL.GetOrdersByStatus(trangThai);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetOrdersByStatus: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy đơn hàng của khách
        /// </summary>
        public List<OrderListViewModel> GetOrdersByCustomer(int maTK)
        {
            if (maTK <= 0)
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ");
            }

            try
            {
                return orderDAL.GetOrdersByCustomer(maTK);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetOrdersByCustomer: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        public bool UpdateOrderStatus(int maDH, string trangThaiMoi)
        {
            if (maDH <= 0)
            {
                throw new ArgumentException("Mã đơn hàng không hợp lệ");
            }

            if (string.IsNullOrWhiteSpace(trangThaiMoi))
            {
                throw new ArgumentException("Trạng thái không hợp lệ");
            }

            try
            {
                return orderDAL.UpdateOrderStatus(maDH, trangThaiMoi);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - UpdateOrderStatus: {ex.Message}", ex);
            }
        }
    }
}
