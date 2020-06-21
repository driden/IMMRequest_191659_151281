export class User {
  constructor(
    public email: string,
    private userToken: string
  ) {}

  get token() {
    return this.userToken;
  }
}
