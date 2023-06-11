export interface AuthResponse {
    nUserID: number,
    userName: string,
    password: string,
    tName: string,
    tEmail: string,
    nRoleType: string,
    auth :AuthModel
}

export interface AuthModel{
    IsAuthSuccessful: boolean,
    ErrorMessage: string,
    Token: string,
    Expires: Date
}
