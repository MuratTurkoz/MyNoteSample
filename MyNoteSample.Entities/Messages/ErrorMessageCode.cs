

namespace MyNoteSample.Entities.Messages
{
    public enum ErrorMessageCode
    {
        UsernameAlreadyExists = 101,
        EmailAlreadyExists = 102,
        UserIsNotActive = 201,
        UsernameOrPassWrong = 202,
        CheckYourEmail = 203,
        UserAlreadyActivate = 303,
        ActivateIdDoesNotExists = 303,
        UserNotFound = 305,
        ProfileCouldNotUpdated = 404,
        UserCouldNotRemove = 405,
        UserCouldNotFind = 406,
        UserCouldNotInserted = 407,
        UserCouldNotUpdated = 408
    }
}
