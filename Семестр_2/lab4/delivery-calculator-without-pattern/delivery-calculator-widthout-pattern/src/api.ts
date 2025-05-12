// src/api.ts

import { DeliveryParams, DeliveryResult } from './services/DeliveryService';

/*
 * Симуляция API-запроса на сервер для расчёта доставки.
 * Возвращает Promise с задержкой, как будто идёт сетевой вызов.
 */
export async function calculateDeliveryFromApi(params: DeliveryParams): Promise<DeliveryResult> {
  return new Promise((resolve) => {
    setTimeout(() => {
      const { weight, length, width, height, distance } = params;

      const volume = length * width * height;
      const price = weight * 10 + volume * 0.05 + distance * 0.1;
      const estimatedDays = Math.ceil(distance / 500);

      resolve({
        price: Math.round(price),
        estimatedDays,
      });
    }, 1200); // Задержка 1200 мс — как будто API отвечает
  });
}
