// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Synapse.QtClient.Windows {
    using System;
    using Qyoto;
    
    
    public partial class ChatWindow : QWidget {
        
        protected QSplitter splitter_2;
        
        protected QSplitter splitter;
        
        protected Synapse.QtClient.ConversationWidget m_ConversationWidget;
        
        protected QWidget bottomContainer;
        
        protected Synapse.QtClient.Widgets.ConversationTextEdit textEdit;
        
        protected QWidget rightContainer;
        
        protected Synapse.QtClient.Widgets.AvatarGrid<jabber.connection.RoomParticipant> participantsGrid;
        
        protected void SetupUi() {
            base.ObjectName = "ChatWindow";
            this.Geometry = new QRect(0, 0, 562, 331);
            this.WindowTitle = "Chat window";
            this.StyleSheet = "";
            QHBoxLayout horizontalLayout;
            horizontalLayout = new QHBoxLayout(this);
            horizontalLayout.Spacing = 0;
            horizontalLayout.Margin = 0;
            this.splitter_2 = new QSplitter(this);
            this.splitter_2.ObjectName = "splitter_2";
            this.splitter_2.Orientation = Qt.Orientation.Horizontal;
            horizontalLayout.AddWidget(this.splitter_2);
            this.splitter = new QSplitter(this.splitter_2);
            this.splitter.ObjectName = "splitter";
            this.splitter.Orientation = Qt.Orientation.Vertical;
            this.splitter.OpaqueResize = true;
            this.splitter.ChildrenCollapsible = false;
            this.splitter_2.AddWidget(this.splitter);
            this.m_ConversationWidget = new Synapse.QtClient.ConversationWidget(this.splitter);
            this.m_ConversationWidget.ObjectName = "m_ConversationWidget";
            this.m_ConversationWidget.Url = new QUrl("about:blank");
            this.splitter.AddWidget(this.m_ConversationWidget);
            this.bottomContainer = new QWidget(this.splitter);
            this.bottomContainer.ObjectName = "bottomContainer";
            QVBoxLayout verticalLayout;
            verticalLayout = new QVBoxLayout(this.bottomContainer);
            verticalLayout.Spacing = 0;
            verticalLayout.Margin = 0;
            this.textEdit = new Synapse.QtClient.Widgets.ConversationTextEdit(this.bottomContainer);
            this.textEdit.ObjectName = "textEdit";
            this.textEdit.FrameShape = QFrame.Shape.NoFrame;
            verticalLayout.AddWidget(this.textEdit);
            this.splitter.AddWidget(this.bottomContainer);
            this.rightContainer = new QWidget(this.splitter_2);
            this.rightContainer.ObjectName = "rightContainer";
            QVBoxLayout verticalLayout_2;
            verticalLayout_2 = new QVBoxLayout(this.rightContainer);
            verticalLayout_2.Margin = 0;
            this.participantsGrid = new Synapse.QtClient.Widgets.AvatarGrid<jabber.connection.RoomParticipant>(this.rightContainer);
            this.participantsGrid.ObjectName = "participantsGrid";
            this.participantsGrid.FrameShape = QFrame.Shape.NoFrame;
            this.participantsGrid.Alignment = ((global::Qyoto.Qyoto.GetCPPEnumValue("Qt", "AlignLeading") | global::Qyoto.Qyoto.GetCPPEnumValue("Qt", "AlignLeft")) | global::Qyoto.Qyoto.GetCPPEnumValue("Qt", "AlignTop"));
            verticalLayout_2.AddWidget(this.participantsGrid);
            this.splitter_2.AddWidget(this.rightContainer);
            QMetaObject.ConnectSlotsByName(this);
        }
    }
}
