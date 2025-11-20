import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { MemberList } from '../features/members/member-list/member-list';
import { MemberDetailed } from '../features/members/member-detailed/member-detailed';
import { List } from '../features/list/list';
import { Messages } from '../features/messages/messages';
import { authGuard } from '../core/guards/auth-guard';

export const routes: Routes = [
    //In Angular application Routes are defined here

    //1 --> Home component has no path. It is the root of the application
    {path: '', component: Home},
    //-->Adding dummy route for Guards
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [

            //2 --> A path of members component
            {path: 'members', component: MemberList},
            //3 -->A path of member detailed component
            {path: 'members/:id', component: MemberDetailed},
            //4 --> A path of list component
            {path: 'lists', component: List},
            //5 --> A path of messages component
            {path: 'messages', component: Messages},
        ]
    },
    //6 --> A path for Wildcard route (Ex.: For a non found page)
    {path: '**', component: Home},
];
