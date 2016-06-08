package internal

import (
	"database/sql"
	"fmt"
	"log"
)

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
