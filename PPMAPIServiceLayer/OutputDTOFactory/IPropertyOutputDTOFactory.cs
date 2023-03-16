﻿using PPMAPIModelLibrary.Properties;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    internal interface IPropertyOutputDTOFactory
    {
        public IPropertyOutputDTO CreatePropertyOutputDTO(IProperty property);
    }
}
