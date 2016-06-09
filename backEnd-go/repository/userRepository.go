package repository

import (
	"database/sql"

	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository/internal"
)

type UserRepository interface {
	Add(user domain.User) int
	Delete(user domain.User)
	FindByID(id int) *domain.User
	FindByEmail(email string) *domain.User
}

type userRepository struct {
	db *sql.DB
}

func NewUserRepository(db *sql.DB) UserRepository {
	result := new(userRepository)
	result.db = db
	return result
}

func (r *userRepository) Add(user domain.User) int {
	return -1 //TODO
}

func (r *userRepository) Delete(user domain.User) {
	//TODO
}

func (r *userRepository) FindByID(id int) *domain.User {
	entity := internal.RunQueryRow(r.db, userFactory, selectUserBase+" WHERE u.[Id] = ?1", id)
	return entity.(*domain.User)
}

func (r *userRepository) FindByEmail(email string) *domain.User {
	entity := internal.RunQueryRow(r.db, userFactory, selectUserBase+" WHERE u.[Email] = ?1", email)
	return entity.(*domain.User)
}

func userFactory() (interface{}, []interface{}) {
	instance := domain.User{}
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
