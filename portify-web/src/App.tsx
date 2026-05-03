import React from 'react';
import Sidebar from './components/layout/Sidebar';
import Dashboard from './pages/Dashboard';

function App() {
  return (
    <div className="flex min-h-screen bg-bg text-[#e2e8f0]">
      <Sidebar />
      <Dashboard />
    </div>
  );
}

export default App;
