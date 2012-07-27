﻿using System;
using System.Linq;
using System.Threading;
using System.Web.Routing;

using SignalR;

namespace TimeSignalR
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start( object sender, EventArgs e )
        {
            RouteTable.Routes.MapConnection<TimeConnection>( "time", "time/{*operation}" );

            ThreadPool.QueueUserWorkItem( _ =>
            {
                var connectionContext = GlobalHost.ConnectionManager.GetConnectionContext<TimeConnection>( );

                while ( true )
                {
                    connectionContext.Connection.Broadcast( DateTime.Now.ToString( ) );
                    Thread.Sleep( 1000 );
                }
            } );
        }
    }
}