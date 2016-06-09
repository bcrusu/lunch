package repository

import (
	"database/sql"

	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository/internal"
)

type UserRepository interface {
	Add(user *domain.User)
	Update(user *domain.User)
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

func (r *userRepository) Add(user *domain.User) {
	ID := internal.ExecInsert(r.db, `INSERT INTO [Users] ([Description], [DisplayName], [Email], [ExternalId], [FirstName], [LastName], [PictureUrl], [Type]) 
		VALUES (?, ?, ?, ?, ?, ?, ?, ?)`, user.Description, user.DisplayName, user.Email, user.ExternalID, user.FirstName, user.LastName,
		user.PictureURL, user.Type)

	user.ID = ID
}

func (r *userRepository) Update(user *domain.User) {
	internal.ExecUpdate(r.db, `UPDATE [Users] SET [Description] = ?, [DisplayName] = ?, [Email] = ?, [ExternalId] = ?, [FirstName] = ?, [LastName] = ?, 
		[PictureUrl] = ?, [Type] = ? WHERE [Id] = ?`, user.Description, user.DisplayName, user.Email, user.ExternalID, user.FirstName, user.LastName,
		user.PictureURL, user.Type, user.ID)
}

func (r *userRepository) FindByID(id int) *domain.User {
	entity := internal.RunQueryRow(r.db, userFactory, selectUserBase+" WHERE u.[Id] = ?", id)
	return entity.(*domain.User)
}

func (r *userRepository) FindByEmail(email string) *domain.User {
	entity := internal.RunQueryRow(r.db, userFactory, selectUserBase+" WHERE u.[Email] = ?", email)
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
