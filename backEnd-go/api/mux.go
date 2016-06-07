package api

import (
	"net/http"

	"github.com/bcrusu/lunch/backEnd-go/api"
	"github.com/bcrusu/lunch/backEnd-go/api/handlers"
	"goji.io"
)

func NewMux() http.Handler {
	mux := goji.NewMux()
	mux.UseC(api.ErrorHandler)
	//TODO: CORS
	//TODO: AUTH
	//TODO: auto save changes middleware

	handlers.ConfigureMux(mux)

	return mux
}
