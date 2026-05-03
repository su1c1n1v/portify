import React from 'react';
import { pies } from '../../data/mock';
import PieCard from './PieCard';

const PiesGrid: React.FC = () => (
  <div className="grid grid-cols-3 gap-5">
    {pies.map((pie) => (
      <PieCard key={pie.id} pie={pie} />
    ))}
  </div>
);

export default PiesGrid;
