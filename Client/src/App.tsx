import './App.css'
import { Outlet } from 'react-router-dom';
import { Wrapper } from './App.style';

function App() {
  return (
    <Wrapper>
      <Outlet/>
    </Wrapper>
  )
}

export default App
