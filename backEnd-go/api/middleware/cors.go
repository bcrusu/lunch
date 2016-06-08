package middleware

import (
	"net/http"

	"github.com/bcrusu/lunch/backEnd-go/configuration"
	"github.com/rs/cors"
)

func NewCorsHandler() func(http.Handler) http.Handler {
	allowedOrigin := configuration.DefaultAccessControlAllowOrigin()

	c := cors.New(cors.Options{
		AllowedOrigins: []string{allowedOrigin},
	})

	return c.Handler
}
