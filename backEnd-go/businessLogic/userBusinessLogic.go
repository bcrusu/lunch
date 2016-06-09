package businessLogic

import "github.com/bcrusu/lunch/backEnd-go/domain"

type UserBusinessLogic interface {
	FindByEmail(email string) domain.User

	CreateUser(externalUserDetails ExternalUserDetails) domain.User
}

func NewUserBusinessLogic() UserBusinessLogic {
	result := new(userBusinessLogic)
	//TODO
	return result
}

type userBusinessLogic struct {
	userRepository xxx.UserRepository
}

func (r *userBusinessLogic) FindByEmail(email string) domain.User {
	return domain.User{} //TODO
}

func (r *userBusinessLogic) CreateUser(externalUserDetails ExternalUserDetails) domain.User {
	return domain.User{} //TODO
}
