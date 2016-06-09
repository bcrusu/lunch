package businessLogic

import (
	"database/sql"
	"fmt"

	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository"
)

type UserBusinessLogic interface {
	FindByEmail(email string) *domain.User
	CreateUser(externalUserDetails ExternalUserDetails) *domain.User
	UpdateUser(user *domain.User)
}

func NewUserBusinessLogic(db *sql.DB) UserBusinessLogic {
	result := new(userBusinessLogic)
	result.userRepository = repository.NewUserRepository(db)
	return result
}

type userBusinessLogic struct {
	userRepository repository.UserRepository
}

func (r *userBusinessLogic) FindByEmail(email string) *domain.User {
	return r.userRepository.FindByEmail(email)
}

func (r *userBusinessLogic) CreateUser(externalUserDetails ExternalUserDetails) *domain.User {
	if !validateUserType(externalUserDetails.UserType) {
		panic(fmt.Errorf("Invalid user type."))
	}

	user := new(domain.User)
	user.ExternalID = externalUserDetails.ID
	user.Type = externalUserDetails.UserType
	user.Email = externalUserDetails.Email
	user.FirstName = externalUserDetails.FirstName
	user.LastName = externalUserDetails.LastName
	user.DisplayName = externalUserDetails.DisplayName
	user.Description = externalUserDetails.Description
	user.PictureURL = externalUserDetails.PictureURL

	r.userRepository.Add(user)
	return user
}

func (r *userBusinessLogic) UpdateUser(user *domain.User) {
	if user.ID < 1 {
		panic(fmt.Errorf("Invalid user ID."))
	}

	r.userRepository.Update(user)
}

func validateUserType(userType domain.UserType) bool {
	switch userType {
	case domain.UserTypeExternalLinkedin:
		return true
	default:
		return false
	}
}
