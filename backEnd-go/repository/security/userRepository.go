package security

import (
	"database/sql"

	"github.com/bcrusu/lunch/backEnd-go/domain/security"
	"github.com/bcrusu/lunch/backEnd-go/repository/internal"
)

type UserRepository interface {
	Add(user security.User) int
	Delete(user security.User)
	FindByID(id int) *security.User
	FindByEmail(email string) *security.User
}

type userRepository struct {
	db *sql.DB
}

func NewUserRepository(db *sql.DB) UserRepository {
	result := new(userRepository)
	result.db = db
	return result
}

func (r *userRepository) Add(user security.User) int {
	return -1 //TODO
}

func (r *userRepository) Delete(user security.User) {
	//TODO
}

func (r *userRepository) FindByID(id int) *security.User {
	entity := internal.RunQueryRow(r.db, userFactory, selectUserBase+" WHERE u.[Id] = ?1", id)
	return entity.(*security.User)
}

func (r *userRepository) FindByEmail(email string) *security.User {
	entity := internal.RunQueryRow(r.db, userFactory, selectUserBase+" WHERE u.[Email] = ?1", email)
	return entity.(*security.User)
}

func userFactory() (interface{}, []interface{}) {
	instance := security.User{}
	scanDest := []interface{}{&instance.ID,
		&instance.Description,
		&instance.DisplayName,
		&instance.Email,
		&instance.ExternalID,
		&instance.FirstName,
		&instance.LastName,
		&instance.PictureURL,
		&instance.Type}

	return &instance, scanDest
}

const selectUserBase = `SELECT [Id],
    [Description], 
	[DisplayName],
	[Email],
	[ExternalId],
	[FirstName],
	[LastName],
	[PictureUrl],
	[Type] FROM [Users] u`
