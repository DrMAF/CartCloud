export class AccountDetails {
  companyName: string | null = '';
  companyEmail: string | null = '';
  companyAddress1: string | null = '';
  companyAddress2: string | null = '';
  zipCode: string | null = '';
  timeZone: string | null = '';
  city: string | null = '';
  state: string | null = '';
  country: string | null = '';
  phoneNumber1: string | null = '';
  phoneNumber2: string | null = '';
  webSite: string | null = '';
  fax: string | null = '';
  productType: string | null = '';
  ExpirationDate: string | null = '';
  userName: string | null = '';
  password: string | null = '';
  activate: boolean | null = false;

  constructor(init?: Partial<AccountDetails>) {
    Object.assign(this, init);
  }
}
