using CommunityToolkit.Mvvm.Messaging.Messages;
using MES.Models.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.message
{
    class AddProjectMessage : ValueChangedMessage<ProjectModel?>
    {
		private ProjectModel? model;

		public ProjectModel? Project
		{
			get { return model; }
			set { model = value; }
		}

		public AddProjectMessage(ProjectModel model): base(model) { this.Project = model; } 
	}
}
