//using Microsoft.AspNetCore.Authentication.JwtBearer;

//namespace EventosAPI.Service
//{
//    public class JwtBearerEventHandler
//    {
//        private readonly JwtService _jwtService;

//        public JwtBearerEventHandler(JwtService jwtService)
//        {
//            _jwtService = jwtService;
//        }

//        public Task TokenValidated(TokenValidatedContext context)
//        {
//            var token = context.Request.Headers["Authorization"].ToString().Split(" ")[1];

//            if (_jwtService.IsTokenRevoked(token))
//            {
//                // Se o token estiver revogado, rejeite a requisição
//                context.Fail("Token has been revoked.");
//            }

//            return Task.CompletedTask;
//        }
//    }

//}
