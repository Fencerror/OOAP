//Интерфейс

import { calculateDeliveryFromApi } from '../api';

export interface DeliveryParams {
  weight: number;
  length: number;
  width: number;
  height: number;
  distance: number;
}

export interface DeliveryResult {
  price: number;
  estimatedDays: number;
}

export interface DeliveryService {
  calculateDelivery(params: DeliveryParams): Promise<DeliveryResult>;
}

export class RealDeliveryService implements DeliveryService {
  calculateDelivery(params: DeliveryParams): Promise<DeliveryResult> {
    return calculateDeliveryFromApi(params);
  }
}
