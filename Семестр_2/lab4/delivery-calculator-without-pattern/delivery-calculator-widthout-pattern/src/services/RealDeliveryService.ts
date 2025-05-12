//Реальный сервис

import { DeliveryParams, DeliveryResult, DeliveryService } from './DeliveryService';
import { calculateDeliveryFromApi } from '../api';

export class RealDeliveryService implements DeliveryService {
  async calculateDelivery(params: DeliveryParams): Promise<DeliveryResult> {
    return await calculateDeliveryFromApi(params);
  }
}
