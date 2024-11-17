using Microsoft.AspNetCore.Components;

namespace Orders.Frontend.Shared
{
    public partial class GenericList<TItem>
    {
        [Parameter]
        public RenderFragment? Loading { get; set; }
        
        [Parameter]
        public RenderFragment? NoRecords { get; set; }
        
        [EditorRequired, Parameter]
        public RenderFragment Body { get; set; } = null!;

        [EditorRequired, Parameter]
        public List<TItem> MyList { get; set; } = null!;
    }
}
