import { CustomerResponse } from "./CustomerResponse";

export class Account {
  accountNumber: string | null = '';
  email: string | null = '';
  name: string | null = '';
  licenseType: string | null = '';
  expirationdate: string | null = '';
  status: string | null = '';
  lastActivity: string | null = '';
  activityLogs = {}
  usedSpace: number | null = 0;

  constructor(init?: Partial<Account>) {
    Object.assign(this, init);
  }

  static creatAccountFromCustomerResponse(response: CustomerResponse): Account {
    return new Account({
      accountNumber: response.customerKey,
      email: response.email,
      name: response.customerName,
      licenseType: "placeHoldr",
      expirationdate: response.expirationDate,
      status: response.deleted == true ? 'Inactive' : response.isDemo == true ? 'Demo' : response.active == true ? "Active" : "Renewal",
      lastActivity: response.lastActivity,
      usedSpace: 9999
    });
  }

  static creatAccountArrayFromCustomerResponse(responseArray: CustomerResponse[]): Account[] {

    if (responseArray == null) {
      let userTestStatus: Account[] = [];

      return userTestStatus;
    }
   
    return responseArray.map(response =>
      new Account({
        accountNumber: response.customerKey,
        email: response.email,
        name: response.customerName,
        licenseType: "placeHoldr",
        expirationdate: response.expirationDate,
        status: response.deleted == true ? 'Inactive' : response.isDemo == true ? 'Demo' : response.active == true ? "Active" : "Renewal",
        lastActivity: response.lastActivity,
        usedSpace: 9999
      }));
  }
}
