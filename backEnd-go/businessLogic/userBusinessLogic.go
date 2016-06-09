package businessLogic

import (
	"database/sql"
	"log"

	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository"
)

type UserBusinessLogic interface {
	FindByEmail(email string) domain.User

	CreateUser(externalUserDetails ExternalUserDetails) domain.User
}

func NewUserBusinessLogic(db *sql.DB) UserBusinessLogic {
	result := new(userBusinessLogic)
	result.userRepository = repository.NewUserRepository(db)
	return result
}

type userBusinessLogic struct {
	userRepository repository.UserRepository
}

func (r *userBusinessLogic) FindByEmail(email string) domain.User {
	return r.userRepository.FindByEmail(email)
}

func (r *userBusinessLogic) CreateUser(externalUserDetails ExternalUserDetails) domain.User {
	if !validateUserType(externalUserDetails.UserType) {
		log.Panicf("Invalid user type.")
	}

	user := domain.User{}
	user.ExternalID = externalUserDetails.ID
	user.Type = externalUserDetails.UserType
	user.Email = externalUserDetails.Email
	user.FirstName = externalUserDetails.FirstName
	user.LastName = externalUserDetails.LastName
	user.DisplayName = externalUserDetails.DisplayName
	user.Description = externalUserDetails.Description
	user.PictureURL = externalUserDetails.PictureURL

	return r.userRepository.Add(user)
}

func validateUserType(userType domain.UserType) bool {
	switch userType {
	case domain.UserTypeExternalLinkedin:
		return true
	default:
		return false
	}
}
