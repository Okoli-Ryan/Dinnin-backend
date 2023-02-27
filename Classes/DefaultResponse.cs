namespace OrderUp_API.Classes {
    public class DefaultResponse<T> {

        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public T ResponseData { get; set; }

        public DefaultResponse() { }

    }

    public class DefaultErrorResponse<T> : DefaultResponse<T> {

        public DefaultErrorResponse() : base() {
            ResponseCode = ResponseCodes.FAILURE;
            ResponseMessage = ResponseMessages.FAILURE;
            ResponseData = default(T);
        }
    }



    public class DefaultSuccessResponse<T> : DefaultResponse<T> {

        public DefaultSuccessResponse(T ResponseData) : base() {
            ResponseCode = ResponseCodes.SUCCESS;
            ResponseMessage = ResponseMessages.SUCCESS;
            this.ResponseData = ResponseData;
        }
    }
}
