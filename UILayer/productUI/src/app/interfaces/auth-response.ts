export interface AuthResponse {
    nUserID: number,
    userName: string,
    password: string,
    tName: string,
    tEmail: string,
    nRoleType: string,
    auth :AuthResponse
}

export interface AuthResponse{
    IsAuthSuccessful: boolean,
    ErrorMessage: string,
    Token: string,
    Expires: Date
}
