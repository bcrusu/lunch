package main

import (
	"log"
	"net/http"

	"github.com/bcrusu/lunch/backEnd-go/api"
)

func main() {
	mux := api.NewMux()

	log.Println("Listening...")
	http.ListenAndServe("localhost:3000", mux)
}
