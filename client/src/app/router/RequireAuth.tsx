import { Navigate, Outlet, useLocation } from 'react-router-dom';
import { useAppSelector } from '../store/configureStore';
import { toast } from 'react-toastify';

interface Props {
    roles?: string[];
}

function RequireAuth({roles}: Props) {
    const {user} = useAppSelector(state => state.account);
    const location = useLocation();

    if(!user) {
        return <Navigate to='/login' state={{from: location}}/>
    }

    if(roles && !roles.some(r => user.roles?.includes(r))){
        toast.error('Not authorized, no access in this area!');
        return <Navigate to='/catalog' />
    }

    return <Outlet />
}

export default RequireAuth