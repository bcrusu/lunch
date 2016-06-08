package main

import (
	"log"
	"net/http"

	"github.com/bcrusu/lunch/backEnd-go/api"
	_ "github.com/denisenkom/go-mssqldb" //register MSSQL driver
)

func init() {

}

func main() {
	mux := api.NewMux()

	log.Println("Listening...")
	http.ListenAndServe("localhost:7777", mux)
}
