//Фиктивная служба

import { DeliveryParams, DeliveryResult, DeliveryService } from './DeliveryService';

export class MockDeliveryService implements DeliveryService {
  async calculateDelivery(params: DeliveryParams): Promise<DeliveryResult> {
    console.log('Mock вызван с:', params);
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const fail = Math.random() < 0.05; // 5% ошибок
        if (fail) {
          reject(new Error('Сбой mock-сервиса'));
        } else {
          resolve({ price: 999, estimatedDays: 3 });
        }
      }, 0);
    });
  }
}
