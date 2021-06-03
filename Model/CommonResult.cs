namespace RentalServer.Model
{
    public class CommonResult<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public static class CommonResult
    {
        public static CommonResult<string> Success(string message)
        {
            return new() {Data = null, Message = message, Status = 200};
        }
        
        public static CommonResult<string> BadRequest(string message)
        {
            return new() {Data = null, Message = message, Status = 400};
        } 
        
        public static CommonResult<T> BadRequest<T>(string message, T data)
        {
            return new() {Data = data, Message = message, Status = 400};
        } 
        
        public static CommonResult<T> Success<T>(T data)
        {
            return new() {Data = data, Message = "请求成功", Status = 200};
        }
        
        public static CommonResult<T> BadRequest<T>(T data)
        {
            return new() {Data = data, Message = "请求失败", Status = 400};
        }
        
        public static CommonResult<T> Success<T>(string message, T data)
        {
            return new() {Data = data, Message = message, Status = 200};
        }
        
    }
}