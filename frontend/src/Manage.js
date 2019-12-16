import React from 'react';
import logo from './logo.svg';

function Manage() {
  return (
    <div>
      <div className="center-container">
        <div className="card">
          <div className="card-header">
            <h1>Add Document</h1>
          </div>
        </div>
        <div className="card">
          <div className="card-header" style={{ backgroundColor: "#f5075a"}}>
            <h1>Revoke Document</h1>
          </div>
        </div>
      </div>
      <div className="center-container">
        <div className="card">
          <div className="card-header">
            <h1>Get Information</h1>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Manage;
