﻿namespace SiT.Scheduler.Core.OperativeModels.Prototype;

using System;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public record IdentityPrototype(Guid Id, Guid TenantId, string DisplayName) : IIdentityPrototype;