namespace OrderUp_API.Utils {
    public class ControllerResponseHandler : ControllerBase {

        public IActionResult HandleResponse<T>(DefaultResponse<T> response) {

            if (response.ResponseCode.Equals(ResponseCodes.SUCCESS)) return Ok(response.ResponseData);
            if (response.ResponseCode.Equals(ResponseCodes.UNAUTHORIZED)) return Unauthorized(response);
            if (response.ResponseCode.Equals(ResponseCodes.USER_ALREADY_EXIST)) return Conflict(response);
            if (response.ResponseCode.Equals(ResponseCodes.INVALID_TOKEN)) return BadRequest(response);
            if (response.ResponseCode.Equals(ResponseCodes.INVALID_CREDENTIALS)) return BadRequest(response);
            if (response.ResponseCode.Equals(ResponseCodes.FAILURE)) return StatusCode(500, response);

            return StatusCode(500, response);
        }
    }

}
