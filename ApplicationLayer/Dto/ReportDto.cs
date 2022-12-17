﻿using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Dto;

public record ReportDto(ICollection<MessageDto> Messages, int Count);