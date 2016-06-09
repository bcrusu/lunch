package domain

type User struct {
	ID          int
	ExternalID  string
	Type        UserType
	Email       string
	FirstName   string
	LastName    string
	DisplayName string
	Description string
	PictureURL  string
}
