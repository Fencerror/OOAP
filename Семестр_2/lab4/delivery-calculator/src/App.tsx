import React, { useState } from 'react';
import './App.css';
import DeliveryForm from './components/DeliveryForm';
import { RealDeliveryService } from './services/RealDeliveryService';
import { MockDeliveryService } from './services/MockDeliveryService';
import { DeliveryService } from './services/DeliveryService';

function App() {
  const [useMock, setUseMock] = useState(false);

  const toggleService = () => setUseMock(!useMock);

  const service: DeliveryService = useMock
    ? new MockDeliveryService()
    : new RealDeliveryService();

  return (
    <div className="app-container">
      <header className="app-header">
        <button className="toggle-btn" onClick={toggleService}>
          {useMock ? 'Реальный сервис' : 'Mock-сервис'}
        </button>
        <h1 className="app-title">Служба доставки BeastDelivery</h1>
      </header>
      <main className="main-content">
        <DeliveryForm service={service} />
      </main>
      <footer className="app-footer">
        <p>© 2025 BeastDelivery - доставим с дикой скоростью!</p>
      </footer>
    </div>
  );
}

export default App;