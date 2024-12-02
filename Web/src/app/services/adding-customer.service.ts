import { Injectable } from '@angular/core';
import { AccountDetails } from '../models/accountDetails';
import { LicenseDetails } from '../models/LicenseDetails';
import { SupportDetails } from '../models/SupportDetails';
import { licenseInformation } from '../models/LicenseInformation';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { environment } from '../../environments/environment.development';
import { CustomerResponse } from '../models/CustomerResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AddingCustomerService {
  accountDetails = new AccountDetails();
  supportDetails = new SupportDetails();
  licenseInformation = new licenseInformation();
  licenseDetails = new LicenseDetails();

  allSource: any = [];
  allTarget: any = [];

  enterpriseAccount: any = {}
  customer: any = {}
  customerUser: any = {}

  customerUserAccount = {
    accountUserId: 0,
    loginToken: "string",
    tokenExpiry: "2024-07-07T13:56:51.263Z"
  }

  enterpriseDB: any = {}

  enterpriseAssetLicense: any = {}
  enterpriseStockLicense: any = {}
  enterpriseGeneralLicense: any = {}
  customerLicense: any = {}

  constructor(private http: HttpClient, private datePipe: DatePipe) { }

  extractAllArrays(obj: any, result: any[] = []): any[] {
    for (const key in obj) {
      if (Array.isArray(obj[key])) {
        result.push(...obj[key]);
      }
      else if (typeof obj[key] === 'object' && obj[key] !== null) {
        this.extractAllArrays(obj[key], result);
      }
    }

    return result;
  }

  setCustomerUser(): void {
    this.customerUser['userPassword'] = this.accountDetails.password;
    this.customerUser['userEmail'] = this.accountDetails.companyEmail;
    this.customerUser['firstName'] = this.accountDetails.userName;
    this.customerUser['lastName'] = this.accountDetails.userName;
    this.customerUser['active'] = this.accountDetails.activate;
    this.customerUser['deleted'] = false;
    this.customerUser['isEnterpriseUser'] = true;
    this.customerUser['passwordHash'] = 'string';
    this.customerUser['passwordSalt'] = 'string';
  }

  setCustomerObject(): void {
    this.customer['customerName'] = this.accountDetails.userName;
    this.customer['supported'] = this.supportDetails.activate;// ask about this
    this.customer['supportContractExpirationDate'] = this.datePipe.transform(this.supportDetails.experationDate, 'yyyy-MM-dd')?.toString() == "" ?
      null : this.datePipe.transform(this.supportDetails.experationDate, 'yyyy-MM-dd');//ask about this

    this.customer['address1'] = this.accountDetails.companyAddress1;
    this.customer['address2'] = this.accountDetails.companyAddress2;
    this.customer['city'] = this.accountDetails.city;
    this.customer['state'] = this.accountDetails.state;
    this.customer['zip'] = this.accountDetails.zipCode;
    this.customer['country'] = this.accountDetails.country;
    this.customer['phone1'] = this.accountDetails.phoneNumber1;
    this.customer['phone2'] = this.accountDetails.phoneNumber2;
    this.customer['fax'] = this.accountDetails.fax;
    this.customer['email'] = this.accountDetails.companyEmail;
    this.customer['email2'] = 'not set';
    this.customer['contactFirstName'] = "string";
    this.customer['contactLastName'] = "string";
    this.customer['contactPhone1'] = this.accountDetails.phoneNumber1;
    this.customer['contactPhone2'] = this.accountDetails.phoneNumber2;
    this.customer['contactFax'] = this.accountDetails.fax;
    this.customer['contactEmail'] = this.accountDetails.companyEmail;
    this.customer['active'] = true;
    this.customer['deleted'] = false;
    this.customer['expirationDate'] = this.datePipe.transform(this.accountDetails.ExpirationDate, 'yyyy-MM-dd')?.toString() == "" ?
      null : this.datePipe.transform(this.accountDetails.ExpirationDate, 'yyyy-MM-dd')?.toString();
    this.customer['isDemo'] = false;
  }

  setCustomerLicense(): void {
    this.customerLicense['NbUsers'] = parseInt(this.licenseInformation.concurrentUsers ?? "");
    this.customerLicense['NbMobileUsers'] = parseInt(this.licenseInformation.mobileDevices ?? "");
    this.customerLicense['NbSites'] = parseInt(this.licenseInformation.totalSites ?? "");
    this.customerLicense['NbRecords'] = parseInt(this.licenseInformation.licenseMaxRecords ?? "");
    this.customerLicense['DatabaseSize'] = 1;//this.licenseInformation.dataBase;//missing in the design
    this.customerLicense['CountLoggedUsers'] = this.licenseInformation.concurrentUsers;
    this.customerLicense['MediaSize'] = this.licenseInformation.licenseMaxMedia;
    this.customerLicense['NbUsersAssetShop'] = parseInt(this.licenseInformation.assetShopUsers ?? "");
    this.customerLicense['NbUsersStockShop'] = parseInt(this.licenseInformation.stockShopUsers ?? "");
    this.customerLicense['NbShopMobileUsers'] = parseInt(this.licenseInformation.stockShopMobileDevices ?? "");
    this.enterpriseGeneralLicense['CountLoggedUsers'] = this.licenseInformation.concurrentUsers;
    this.enterpriseGeneralLicense['NumberOfSites'] = parseInt(this.licenseInformation.totalSites ?? "");
    this.enterpriseGeneralLicense['NumberOfMobileUsers'] = parseInt(this.licenseInformation.mobileDevices ?? "");
    this.enterpriseGeneralLicense['NumberOfUsers'] = parseInt(this.licenseInformation.concurrentUsers ?? "");
    this.enterpriseGeneralLicense['DatabaseSize'] = 1;//this.licenseInformation.dataBase;//missing in the design
    this.enterpriseGeneralLicense['NumberOfRecords'] = parseInt(this.licenseInformation.licenseMaxRecords ?? "");
  }

  setAccountObject(): void {
    this.enterpriseAccount['enterpriseAccountName'] = this.accountDetails.companyName;
    this.enterpriseAccount['enterpriseAllowedAccounts'] = this.licenseInformation.concurrentUsers == "" ? 0 : this.licenseInformation.concurrentUsers;
    this.enterpriseAccount['enterpriseAccountActive'] = this.accountDetails.activate;
    this.enterpriseAccount['enterpriseAccountDeleted'] = false;
    this.enterpriseAccount['enterpriseParentId'] = 0;
    this.enterpriseAccount['isEnterprise'] = false;
  }

  setLicenseObject(): void {
    this.allSource.forEach((x: any) => {
      if (x.requestObject == 'enterpriseAssetLicense') {
        this.enterpriseAssetLicense[x.value] = false;
      }

      if (x.requestObject == 'enterpriseStockLicense') {
        this.enterpriseStockLicense[x.value] = false;

      }

      if (x.requestObject == 'enterpriseGeneralLicense') {
        this.enterpriseGeneralLicense[x.value] = false;
      }
    });

    this.allTarget.forEach((x: any) => {
      if (x.requestObject == 'enterpriseAssetLicense') {
        this.enterpriseAssetLicense[x.value] = true;
      }

      if (x.requestObject == 'enterpriseStockLicense') {
        this.enterpriseStockLicense[x.value] = true;
      }

      if (x.requestObject == 'enterpriseGeneralLicense') {
        this.enterpriseGeneralLicense[x.value] = true;
      }
    });

    this.enterpriseStockLicense['ModifiedBy'] = "system";
    this.enterpriseStockLicense['CreatedBy'] = "system";
  }

  setEnterpriseDB(): void {
    this.enterpriseDB['enterpriseDBServer'] = this.licenseInformation.dataBase;
    this.enterpriseDB['enterpriseDBUser'] = this.licenseInformation.userName;
    this.enterpriseDB['enterpriseDBPassword'] = this.licenseInformation.password;
  }

  sendRequest() {
    return this.http.post<any>(`${environment.APIUrl}/Customers/CreateCustomer`, {
      customerUserAccount: this.customerUserAccount,
      enterpriseAccount: this.enterpriseAccount,
      customer: this.customer,
      customerUser: this.customerUser,
      enterpriseDB: this.enterpriseDB,
      enterpriseAssetLicense: this.enterpriseAssetLicense,
      enterpriseStockLicense: this.enterpriseStockLicense,
      enterpriseGeneralLicense: this.enterpriseGeneralLicense,
      customerLicense: this.customerLicense
    });
  }

  reset(): void {
    this.accountDetails = new AccountDetails();
    this.supportDetails = new SupportDetails();
    this.licenseInformation = new licenseInformation();
    this.licenseDetails = new LicenseDetails();

    this.allSource = [];
    this.allTarget = [];

    this.enterpriseAccount = {}
    this.customer = {}
    this.customerUser = {}
    this.customerLicense = {}

    this.enterpriseAssetLicense = {}
    this.enterpriseStockLicense = {}
    this.enterpriseGeneralLicense = {}
    this.customerLicense = {}
  }

  //allArrays = this.extractAllArrays(source);

  getCustomerByEmail(email: string): Observable<CustomerResponse> {
    return this.http.get<CustomerResponse>(`${environment.APIUrl}/Customers/GetCustomerByEmail?email=${email}`)
  }

  updateCustomer(customer: CustomerResponse) {
    return this.http.put<any>(`${environment.APIUrl}/Customers/UpdateCustomer`, {
      customer: { email: customer.contactEmail },
      enterpriseGeneralLicense: { numberOfSites: customer.numberOfSites }
    });
  }
}

