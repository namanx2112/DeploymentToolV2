import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { StoreAudio, StoreConfiguration, StoreContact, StoreExteriorMenus, StoreInstallation, StoreInteriorMenus, StoreNetworkings, StorePOS, StorePaymentSystem, StoreSonicRadio, StoreStackholders, StoreServerHandheld, StoreNetworkSwitch, StoreImageMemory } from '../interfaces/store';
import { Dictionary } from '../interfaces/commons';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class AllTechnologyComponentsService {

  constructor(private http: HttpClient, private cacheService: CacheService) {
  }

  GetStoreContact(request: Dictionary<string>) {
    return this.http.post<StoreContact[]>(CommonService.ConfigUrl + "StoreContacts/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateStoreContact(request: StoreContact) {
    return this.http.post<StoreContact>(CommonService.ConfigUrl + "StoreContacts/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateStoreContact(request: StoreContact) {
    return this.http.post<StoreContact>(CommonService.ConfigUrl + "StoreContacts/update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetStoreConfig(request: Dictionary<string>) {
    return this.http.post<StoreConfiguration[]>(CommonService.ConfigUrl + "ProjectConfigs/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateStoreConfig(request: StoreConfiguration) {
    return this.http.post<StoreConfiguration>(CommonService.ConfigUrl + "ProjectConfigs/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateStoreConfig(request: StoreConfiguration) {
    return this.http.post<StoreConfiguration>(CommonService.ConfigUrl + "ProjectConfigs/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetStackholders(request: Dictionary<string>) {
    return this.http.post<StoreStackholders[]>(CommonService.ConfigUrl + "ProjectStakeHolders/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateStackholders(request: StoreStackholders) {
    return this.http.post<StoreStackholders>(CommonService.ConfigUrl + "ProjectStakeHolders/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateStackholders(request: StoreStackholders) {
    return this.http.post<StoreStackholders>(CommonService.ConfigUrl + "ProjectStakeHolders/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetNetworking(request: Dictionary<string>) {
    return this.http.post<StoreNetworkings[]>(CommonService.ConfigUrl + "ProjectNetworkings/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateNetworking(request: StoreNetworkings) {
    return this.http.post<StoreNetworkings>(CommonService.ConfigUrl + "ProjectNetworkings/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateNetworking(request: StoreNetworkings) {
    return this.http.post<StoreNetworkings>(CommonService.ConfigUrl + "ProjectNetworkings/update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetPOS(request: Dictionary<string>) {
    return this.http.post<StorePOS[]>(CommonService.ConfigUrl + "ProjectPOS/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreatePOS(request: StorePOS) {
    return this.http.post<StorePOS>(CommonService.ConfigUrl + "ProjectPOS/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdatePOS(request: StorePOS) {
    return this.http.post<StorePOS>(CommonService.ConfigUrl + "ProjectPOS/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetAudio(request: Dictionary<string>) {
    return this.http.post<StoreAudio[]>(CommonService.ConfigUrl + "ProjectAudios/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateAudio(request: StoreAudio) {
    return this.http.post<StoreAudio>(CommonService.ConfigUrl + "ProjectAudios/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateAudio(request: StoreAudio) {
    return this.http.post<StoreAudio>(CommonService.ConfigUrl + "ProjectAudios/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetExteriorMenus(request: Dictionary<string>) {
    return this.http.post<StoreExteriorMenus[]>(CommonService.ConfigUrl + "ProjectExteriorMenus/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateExteriorMenus(request: StoreExteriorMenus) {
    return this.http.post<StoreExteriorMenus>(CommonService.ConfigUrl + "ProjectExteriorMenus/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateExteriorMenus(request: StoreExteriorMenus) {
    return this.http.post<StoreExteriorMenus>(CommonService.ConfigUrl + "ProjectExteriorMenus/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetPaymentSystem(request: Dictionary<string>) {
    return this.http.post<StorePaymentSystem[]>(CommonService.ConfigUrl + "ProjectPaymentSystems/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreatePaymentSystem(request: StorePaymentSystem) {
    return this.http.post<StorePaymentSystem>(CommonService.ConfigUrl + "ProjectPaymentSystems/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdatePaymentSystem(request: StorePaymentSystem) {
    return this.http.post<StorePaymentSystem>(CommonService.ConfigUrl + "ProjectPaymentSystems/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetInteriorMenus(request: Dictionary<string>) {
    return this.http.post<StoreInteriorMenus[]>(CommonService.ConfigUrl + "ProjectInteriorMenus/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateInteriorMenus(request: StoreInteriorMenus) {
    return this.http.post<StoreInteriorMenus>(CommonService.ConfigUrl + "ProjectInteriorMenus/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateInteriorMenus(request: StoreInteriorMenus) {
    return this.http.post<StoreInteriorMenus>(CommonService.ConfigUrl + "ProjectInteriorMenus/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetSonicRadio(request: Dictionary<string>) {
    return this.http.post<StoreSonicRadio[]>(CommonService.ConfigUrl + "ProjectSonicRadios/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateSonicRadio(request: StoreSonicRadio) {
    return this.http.post<StoreSonicRadio>(CommonService.ConfigUrl + "ProjectSonicRadios/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateSonicRadio(request: StoreSonicRadio) {
    return this.http.post<StoreSonicRadio>(CommonService.ConfigUrl + "ProjectSonicRadios/update", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetInstallation(request: Dictionary<string>) {
    return this.http.post<StoreInstallation[]>(CommonService.ConfigUrl + "ProjectInstallations/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateInstallation(request: StoreInstallation) {
    return this.http.post<StoreInstallation>(CommonService.ConfigUrl + "ProjectInstallations/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateInstallation(request: StoreInstallation) {
    return this.http.post<StoreInstallation>(CommonService.ConfigUrl + "ProjectInstallations/update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetServerHandheld(request: Dictionary<string>) {
    return this.http.post<StoreServerHandheld[]>(CommonService.ConfigUrl + "ProjectServerHandheld/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateServerHandheld(request: StoreServerHandheld) {
    return this.http.post<StoreServerHandheld>(CommonService.ConfigUrl + "ProjectServerHandheld/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateServerHandheld(request: StoreServerHandheld) {
    return this.http.post<StoreServerHandheld>(CommonService.ConfigUrl + "ProjectServerHandheld/update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetNetworkSwitch(request: Dictionary<string>) {
    return this.http.post<StoreNetworkSwitch[]>(CommonService.ConfigUrl + "ProjectNetworkSwitch/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateNetworkSwitch(request: StoreNetworkSwitch) {
    return this.http.post<StoreNetworkSwitch>(CommonService.ConfigUrl + "ProjectNetworkSwitch/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateNetworkSwitch(request: StoreNetworkSwitch) {
    return this.http.post<StoreNetworkSwitch>(CommonService.ConfigUrl + "ProjectNetworkSwitch/update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetImageMemory(request: Dictionary<string>) {
    return this.http.post<StoreImageMemory[]>(CommonService.ConfigUrl + "ProjectImageMemory/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }
  CreateImageMemory(request: StoreImageMemory) {
    return this.http.post<StoreImageMemory>(CommonService.ConfigUrl + "ProjectImageMemory/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }
  UpdateImageMemory(request: StoreImageMemory) {
    return this.http.post<StoreImageMemory>(CommonService.ConfigUrl + "ProjectImageMemory/update", request, { headers: this.cacheService.getHttpHeaders() });
  }
}
