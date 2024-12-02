export class LoginRequest {
  email: string | null = "";
  password: string | null = "";
  constructor(init?: Partial<LoginRequest>) {
    Object.assign(this, init);
  }
}
