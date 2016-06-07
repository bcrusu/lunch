package api

import (
	"log"
	"net/http"

	"goji.io"
	"golang.org/x/net/context"
)

func ErrorHandler(inner goji.Handler) goji.Handler {
	mw := func(ctx context.Context, responseWriter http.ResponseWriter, request *http.Request) {
		defer recoverError(responseWriter, request)
		inner.ServeHTTPC(ctx, responseWriter, request)
	}
	return goji.HandlerFunc(mw)
}

func recoverError(responseWriter http.ResponseWriter, request *http.Request) {
	if r := recover(); r != nil {
		log.Printf("Error processing request %s. Error: %v", request.RequestURI, r)

		responseWriter.WriteHeader(500)
		responseWriter.Write([]byte("Internal server error."))
	}
}
