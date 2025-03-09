import HomePage from './pages/homePage'
import PostPage from './pages/postPage'
import RegisterPage from './pages/registerPage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import ProfilePage from './pages/profilePage';
import { PostProvider } from './context/postAppProvider';
import ProtectedRoute from './components/protectedRoute';

function App() {
  return (
    <BrowserRouter>
      <PostProvider>
        <Routes>
          {/* no protected */}
          <Route path="/" element={<HomePage />} />
          <Route path="/register" element={<RegisterPage />} />

          {/* protected */}

          <Route path="/post" element={<ProtectedRoute children={<PostPage />} />} />

          <Route path="/profile" element={<ProtectedRoute children={<ProfilePage />} />} />
            
        </Routes>
      </PostProvider>
    </BrowserRouter>
  )
}

export default App;
