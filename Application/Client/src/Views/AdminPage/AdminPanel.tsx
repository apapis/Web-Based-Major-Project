import React from "react";
import { Outlet } from "react-router-dom";

const AdminPanel: React.FC = () => {
  return (
    <div>
      <Outlet />
    </div>
  );
};

export default AdminPanel;
