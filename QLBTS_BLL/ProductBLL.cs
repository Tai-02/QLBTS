using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBTS_DTO;
using QLBTS_DAL;

namespace QLBTS_BLL
{
    public class ProductBLL
    {
        private ProductDAL productDAL;

        public ProductBLL()
        {
            productDAL = new ProductDAL();
        }

        /// <summary>
        /// Lấy chi tiết sản phẩm
        /// </summary>
        public ProductDetailViewModel GetProductDetail(int maSP)
        {
            if (maSP <= 0)
            {
                throw new ArgumentException("Mã sản phẩm không hợp lệ");
            }

            try
            {
                var product = productDAL.GetProductDetail(maSP);

                if (product == null)
                {
                    throw new Exception("Không tìm thấy sản phẩm");
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetProductDetail: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy tất cả size của sản phẩm
        /// </summary>
        public List<ProductDetailViewModel> GetProductAllSizes(string tenSP)
        {
            if (string.IsNullOrWhiteSpace(tenSP))
            {
                throw new ArgumentException("Tên sản phẩm không hợp lệ");
            }

            try
            {
                return productDAL.GetProductAllSizes(tenSP);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - GetProductAllSizes: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Kiểm tra số lượng có hợp lệ không
        /// </summary>
        public bool ValidateQuantity(int quantity, int maxStock)
        {
            return quantity > 0 && quantity <= maxStock;
        }
    }
}
