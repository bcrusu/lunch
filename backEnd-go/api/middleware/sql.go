package middleware

import (
	"database/sql"
	"log"
	"net/http"

	"github.com/bcrusu/lunch/backEnd-go/configuration"
	"goji.io"
	"golang.org/x/net/context"
)

const ContextKeyDB = "_lunch_DB"
const sqlDriverName = "mssql"

func SQLTransaction(inner goji.Handler) goji.Handler {
	mw := func(ctx context.Context, responseWriter http.ResponseWriter, request *http.Request) {
		connectionString := configuration.ConnectionString()

		// open db connection
		db, err := sql.Open(sqlDriverName, connectionString)
		if err != nil {
			log.Panicf("Error opening database connection: %v", err)
		}

		defer closeDb(db)

		// ensure read commited isolation level is the default for the connection; database/sql API does not allow choosing the level at db.Begin() call
		setTransactionIsolationLevel(db)

		// start transaction
		tx, err := db.Begin()
		if err != nil {
			log.Panicf("Error beginning transaction: %v", err)
		}

		defer closeTransaction(tx, needsCommit(request))

		// add the database object to context
		newCtx := context.WithValue(ctx, ContextKeyDB, db)

		inner.ServeHTTPC(newCtx, responseWriter, request)
	}
	return goji.HandlerFunc(mw)
}

func closeDb(db *sql.DB) {
	err := db.Close()
	if err != nil {
		log.Printf("Error closing the database connection: %v", err)
	}
}

func closeTransaction(tx *sql.Tx, needsCommit bool) {
	if err := recover(); err != nil {
		rollbackErr := tx.Rollback()
		if rollbackErr != nil {
			log.Printf("Error rolling back the transaction: %v", rollbackErr)
		}

		// panic with the original error and allow the errorHandler middleware to do its thing
		panic(err)
	}

	if !needsCommit {
		return
	}

	err := tx.Commit()
	if err != nil {
		log.Panicf("Error commiting the transaction: %v", err)
	}
}

func setTransactionIsolationLevel(db *sql.DB) {
	_, err := db.Exec("SET TRANSACTION ISOLATION LEVEL READ COMMITTED")
	if err != nil {
		log.Panicf("Error setting the transaction isolation level: %v", err)
	}
}

func needsCommit(request *http.Request) bool {
	switch request.Method {
	case "POST", "PUT", "DELETE":
		return true
	default:
		return false
	}
}
