//using ProductAPI.Data;
//using ProductAPI.Model;

//namespace ProductAPI.Repository
//{
//    public class ProductRepository
//    {
//        private readonly ProductAPIContext _dbcontext;
//        public ProductRepository(ProductAPIContext dbcontext)
//        {
//            _dbcontext = dbcontext;
//        }

//        public Product CreateStudents(Product student)
//        {
//            _dbcontext.Add(student);
//            _dbcontext.SaveChanges();

//            return student;
//        }

//        public List<Product> GetAll()
//        {
//            return _dbcontext.Product.ToList();
//        }
//    }
//}
