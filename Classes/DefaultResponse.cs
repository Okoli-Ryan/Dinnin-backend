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
            ResponseData = default;
        }

        public DefaultErrorResponse(T response) : base() {
            ResponseCode = ResponseCodes.FAILURE;
            ResponseMessage = ResponseMessages.FAILURE;
            ResponseData = response;
        }

    }



    public class DefaultSuccessResponse<T> : DefaultResponse<T> {

        public DefaultSuccessResponse(T ResponseData) : base() {
            ResponseCode = ResponseCodes.SUCCESS;
            ResponseMessage = ResponseMessages.SUCCESS;
            this.ResponseData = ResponseData;
        }
    }

    public class DefaultNotFoundResponse<T> : DefaultErrorResponse<T> {

        public DefaultNotFoundResponse(string ErrorMessage = ResponseMessages.NOT_FOUND) : base() {
            ResponseCode = ResponseCodes.NOT_FOUND;
            ResponseMessage = ErrorMessage;
            ResponseData = default;
        }
    }
    
    public class DefaultUnauthorizedResponse<T> : DefaultErrorResponse<T> {

        public DefaultUnauthorizedResponse(string ErrorMessage = ResponseMessages.UNAUTHORIZED) : base() {
            ResponseCode = ResponseCodes.UNAUTHORIZED;
            ResponseMessage = ErrorMessage;
            ResponseData = default;
        }
    }

    public class DefaultFailureResponse<T> : DefaultErrorResponse<T> {

        public DefaultFailureResponse(string ErrorMessage = ResponseMessages.FAILURE) : base() {
            ResponseCode = ResponseCodes.FAILURE;
            ResponseMessage = ErrorMessage;
            ResponseData = default;
        }

    }

    public class DefaultFailurePaginationResponse<T> : DefaultErrorResponse<PaginatedResponse<T>> {

        public DefaultFailurePaginationResponse(string ErrorMessage = ResponseMessages.FAILURE) : base() {
            ResponseCode = ResponseCodes.FAILURE;
            ResponseMessage = ErrorMessage;
            ResponseData = new PaginatedResponse<T>() {
                Data = {},
                Page = 1,
                Size = 1,
                Total = 1,
            };
        }

    }

    public class DefaultInvalidTokenResponse<T> : DefaultErrorResponse<T> {

        public DefaultInvalidTokenResponse(string ErrorMessage = ResponseMessages.INVALID_TOKEN) : base() {
            ResponseCode = ResponseCodes.INVALID_TOKEN;
            ResponseMessage = ErrorMessage;
            ResponseData = default;
        }
    }
}
