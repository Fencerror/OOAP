// src/components/DeliveryForm.tsx

import React, { useState } from 'react';
import { DeliveryParams, DeliveryResult, DeliveryService } from '../services/DeliveryService';
import './DeliveryForm.css';

interface Props {
  service: DeliveryService;
}

const DeliveryForm: React.FC<Props> = ({ service }) => {
  const [form, setForm] = useState<DeliveryParams>({
    weight: 0,
    length: 0,
    width: 0,
    height: 0,
    distance: 0,
  });

  const [result, setResult] = useState<DeliveryResult | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    const numValue = parseFloat(value) || 0;

    setForm((prevForm) => ({
      ...prevForm,
      [name]: numValue,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setResult(null);
    setError(null);
    setLoading(true);
    try {
      const res = await service.calculateDelivery(form);
      setResult(res);
    } catch (err: any) {
      setError(err.message || 'Произошла ошибка');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="delivery-form-container">
      <h2>Калькулятор стоимости доставки</h2>
      <form className="delivery-form" onSubmit={handleSubmit}>
        <div className="input-group">
          <label htmlFor="weight">Вес, кг:</label>
          <input
            type="number"
            id="weight"
            name="weight"
            value={form.weight}
            onChange={handleChange}
            required
            step="any"
          />
        </div>
        <div className="input-group">
          <label htmlFor="length">Длина, м:</label>
          <input
            type="number"
            id="length"
            name="length"
            value={form.length}
            onChange={handleChange}
            required
            step="any"
          />
        </div>
        <div className="input-group">
          <label htmlFor="width">Ширина, м:</label>
          <input
            type="number"
            id="width"
            name="width"
            value={form.width}
            onChange={handleChange}
            required
            step="any"
          />
        </div>
        <div className="input-group">
          <label htmlFor="height">Высота, м:</label>
          <input
            type="number"
            id="height"
            name="height"
            value={form.height}
            onChange={handleChange}
            required
            step="any"
          />
        </div>
        <div className="input-group">
          <label htmlFor="distance">Расстояние, км:</label>
          <input
            type="number"
            id="distance"
            name="distance"
            value={form.distance}
            onChange={handleChange}
            required
            step="any"
          />
        </div>
        <button type="submit" className="calc-button">
          Рассчитать
        </button>
      </form>

      {loading && <p>Загрузка...</p>}
      {error && <p className="error-msg">Ошибка: {error}</p>}
      {result && (
        <div className="result">
          <h3>Результат:</h3>
          <p>Стоимость: {result.price} руб.</p>
          <p>Срок: {result.estimatedDays} дней</p>
        </div>
      )}
    </div>
  );
};

export default DeliveryForm;
