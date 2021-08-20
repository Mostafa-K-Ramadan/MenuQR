namespace Application.Core
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public short Code { get; set; }        
        public string Message { get; set; }
        
        #nullable enable
        public T? Data { get; set; }

        public static Response<T> MakeResponse(bool isSuccess, string message, T data, short code) => 
                                new Response<T> {IsSuccess = isSuccess, Code = code, Message = message, Data = data};
        public static Response<T> MakeResponse(bool isSuccess, string message, short code) => 
                                new Response<T> {IsSuccess = isSuccess, Code = code, Message = message};

    }
}