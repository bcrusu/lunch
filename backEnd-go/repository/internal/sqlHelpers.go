package internal

import (
	"database/sql"
	"fmt"
	"log"
)

//TODO: create wrapper type over sql.DB to expose below methods

type EntityFactory func() (entity interface{}, scanDest []interface{})

func RunQueryRow(db *sql.DB, entityFactory EntityFactory, query string, args ...interface{}) interface{} {
	row := db.QueryRow(query, args...)

	entity, scanDest := entityFactory()

	err := row.Scan(scanDest...)
	if err != nil {
		panic(fmt.Errorf("Error executing query: %s. %s", query, err))
	}

	return entity
}

func RunQuery(db *sql.DB, entityFactory EntityFactory, query string, args ...interface{}) []interface{} {
	rows, err := db.Query(query, args...)
	defer closeRows(rows)

	if err != nil {
		panic(fmt.Errorf("Error executing query: %s. %s", query, err))
	}

	var result []interface{}

	for rows.Next() {
		entity, scanDest := entityFactory()

		err := rows.Scan(scanDest...)
		if err != nil {
			panic(fmt.Errorf("Error scanning row: %s", err))
		}

		result = append(result, entity)
	}

	return result
}

func closeRows(rows *sql.Rows) {
	err := rows.Close()
	if err != nil {
		log.Printf("Error closing rows: %s", err)
	}
}

func ExecInsert(db *sql.DB, query string, args ...interface{}) int64 {
	res, err := db.Exec(query, args...)

	if err != nil {
		log.Panicf("Error running insert query: %s", err)
	}

	rows, err := res.RowsAffected()
	if err != nil {
		log.Panicf("Error getting number of affected rows: %s", err)
	}

	if rows != 1 {
		log.Panicf("Unexpected number of affected rows. Expected 1 got %d", rows)
	}

	lastID, err := res.LastInsertId()
	if err != nil {
		return lastID
	}

	return 0
}

func ExecScalar(db *sql.DB, query string, args ...interface{}) interface{} {
	row := db.QueryRow(query, args...)

	var scalarValue interface{}
	err := row.Scan(&scalarValue)
	if err != nil {
		panic(fmt.Errorf("Error executing query: %s. %s", query, err))
	}

	return scalarValue
}
