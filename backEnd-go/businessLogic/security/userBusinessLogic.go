package security

import "github.com/bcrusu/lunch/backEnd-go/domain/security"

type UserBusinessLogic interface {
	FindByEmail(email string) security.User

	CreateUser(externalUserDetails ExternalUserDetails) security.User
}

type userBusinessLogic struct {
}

func (r *userBusinessLogic) FindByEmail(email string) security.User {
	return security.User{} //TODO
}

func (r *userBusinessLogic) CreateUser(externalUserDetails ExternalUserDetails) security.User {
	return security.User{} //TODO
}
